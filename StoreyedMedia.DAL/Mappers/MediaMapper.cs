using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class MediaMapper : IDataMapper
    {

        #region Variables

        private int _ordinalMediaTypeId;
        private int _ordinalMediaType;
        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {
            _ordinalMediaTypeId = reader.GetOrdinal("MediaTypeId");
            _ordinalMediaType = reader.GetOrdinal("MediaType");
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

            Media dto = new Media();
            //load the data 
            if (!reader.IsDBNull(_ordinalMediaTypeId)) { dto.MediaTypeId = reader.GetInt32(_ordinalMediaTypeId); }
            if (!reader.IsDBNull(_ordinalMediaType)) { dto.MediaType = reader.GetString(_ordinalMediaType); }
            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
