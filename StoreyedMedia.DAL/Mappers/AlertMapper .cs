using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class AlertMapper : IDataMapper
    {

        #region Variables

        private int _ordinalSearchId;
        private int _ordinalSearchName;
        private int _ordinalKeywords;
        private int _ordinalStatus;
        private int _ordinalIsEnabled;
        private bool _isInitialized = false;
         
        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {

            _ordinalSearchId = ColumnExists(reader, "SearchId") ? reader.GetOrdinal("SearchId") : CommonBase.NonExistantOrdinal;
            _ordinalSearchName = ColumnExists(reader, "SearchName") ? reader.GetOrdinal("SearchName") : CommonBase.NonExistantOrdinal;
            _ordinalKeywords = ColumnExists(reader, "Keywords") ? reader.GetOrdinal("Keywords") : CommonBase.NonExistantOrdinal;
            _ordinalStatus = ColumnExists(reader, "Status") ? reader.GetOrdinal("Status") : CommonBase.NonExistantOrdinal;
            _ordinalIsEnabled = ColumnExists(reader, "IsEnabled") ? reader.GetOrdinal("IsEnabled") : CommonBase.NonExistantOrdinal;

        }

        private static bool ColumnExists(IDataReader reader, string columnName)
        {
            using (var schemaTable = reader.GetSchemaTable())
            {
                if (schemaTable != null)
                    schemaTable.DefaultView.RowFilter = String.Format("ColumnName= '{0}'", columnName);

                return schemaTable != null && (schemaTable.DefaultView.Count > 0);
            }
        }

        public Object GetData(IDataReader reader)
        {
            
            if (!_isInitialized) { InitializeMapper(reader); }
            
            Alert dto = new Alert();
            //load the data 

            if (CommonBase.NonExistantOrdinal != _ordinalSearchId && !reader.IsDBNull(_ordinalSearchId)) { dto.SearchId = reader.GetInt32(_ordinalSearchId); }
            if (CommonBase.NonExistantOrdinal != _ordinalSearchName && !reader.IsDBNull(_ordinalSearchName)) { dto.SearchName = reader.GetString(_ordinalSearchName); }
            if (CommonBase.NonExistantOrdinal != _ordinalKeywords && !reader.IsDBNull(_ordinalKeywords))   { dto.Keywords = reader.GetString(_ordinalKeywords); }
            if (CommonBase.NonExistantOrdinal != _ordinalStatus && !reader.IsDBNull(_ordinalStatus))   { dto.Status = reader.GetByte(_ordinalStatus); }
            //if (!reader.IsDBNull(_ordinalIsEnabled)) { dto.Email = reader.GetString(_ordinalIsEnabled); } 
           
            return dto; 
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
