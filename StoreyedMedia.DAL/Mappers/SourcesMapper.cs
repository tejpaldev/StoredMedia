using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class SourcesMapper : IDataMapper
    {

        #region Variables

        private int _ordinalSourceId;
        private int _ordinalSourceName;
        private int _ordinalSourceLink;
        private int _ordinalSourceType;
        private int _ordinalSearchId;
        private int _ordinalDarkLogo;
        private int _ordinalLightLogo;
        private int _ordinalItemsPublished;
        private int _ordinalStatus;
        private bool _isInitialized = false;
        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {

            _ordinalSourceId = ColumnExists(reader, "SourceId") ? reader.GetOrdinal("SourceId") : CommonBase.NonExistantOrdinal;

            _ordinalSourceName = ColumnExists(reader, "SourceName") ? reader.GetOrdinal("SourceName") : CommonBase.NonExistantOrdinal;

            _ordinalDarkLogo = ColumnExists(reader, "DarkLogo") ? reader.GetOrdinal("DarkLogo") : CommonBase.NonExistantOrdinal;

            _ordinalLightLogo = ColumnExists(reader, "LightLogo") ? reader.GetOrdinal("LightLogo") : CommonBase.NonExistantOrdinal;

            _ordinalItemsPublished = ColumnExists(reader, "ItemsPublished") ? reader.GetOrdinal("ItemsPublished") : CommonBase.NonExistantOrdinal;

            _ordinalSourceLink = ColumnExists(reader, "SourceLink") ? reader.GetOrdinal("SourceLink") : CommonBase.NonExistantOrdinal;

            _ordinalSourceType = ColumnExists(reader, "SourceType") ? reader.GetOrdinal("SourceType") : CommonBase.NonExistantOrdinal;

            _ordinalSearchId = ColumnExists(reader, "SearchId") ? reader.GetOrdinal("SearchId") : CommonBase.NonExistantOrdinal;

            _ordinalStatus = ColumnExists(reader, "Status") ? reader.GetOrdinal("Status") : CommonBase.NonExistantOrdinal;

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

            Source dto = new Source();
            //load the data 
            if (CommonBase.NonExistantOrdinal != _ordinalSourceId && !reader.IsDBNull(_ordinalSourceId)) { dto.SourceId = reader.GetInt32(_ordinalSourceId); }
            if (CommonBase.NonExistantOrdinal != _ordinalSourceName && !reader.IsDBNull(_ordinalSourceName)) { dto.SourceName = reader.GetString(_ordinalSourceName); }
            if (CommonBase.NonExistantOrdinal != _ordinalSourceLink && !reader.IsDBNull(_ordinalSourceLink)) { dto.SourceLink = reader.GetString(_ordinalSourceLink); }
            if (CommonBase.NonExistantOrdinal != _ordinalSourceType && !reader.IsDBNull(_ordinalSourceType)) { dto.SourceType = reader.GetInt32(_ordinalSourceType); }
            if (CommonBase.NonExistantOrdinal != _ordinalSearchId && !reader.IsDBNull(_ordinalSearchId)) { dto.SearchId = reader.GetInt32(_ordinalSearchId); }

            if (CommonBase.NonExistantOrdinal != _ordinalDarkLogo && !reader.IsDBNull(_ordinalDarkLogo)) { dto.DarkLogo = reader.GetString(_ordinalDarkLogo); }
            if (CommonBase.NonExistantOrdinal != _ordinalLightLogo && !reader.IsDBNull(_ordinalLightLogo)) { dto.LightLogo = reader.GetString(_ordinalLightLogo); }
            if (CommonBase.NonExistantOrdinal != _ordinalItemsPublished && !reader.IsDBNull(_ordinalItemsPublished)) { dto.ItemsPublished = reader.GetInt32(_ordinalItemsPublished); }
            if (CommonBase.NonExistantOrdinal != _ordinalStatus && !reader.IsDBNull(_ordinalStatus)) { dto.Status = reader.GetByte(_ordinalStatus); }

            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
