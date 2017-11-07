using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class CategoriesMapper : IDataMapper
    {

        #region Variables

        private int _ordinalCategoryId;
        private int _ordinalCategory;
        private int _ordinalIconUrl;

        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {

            _ordinalCategoryId = reader.GetOrdinal("CategoryId");
            _ordinalCategory = reader.GetOrdinal("Category");
            _ordinalIconUrl = reader.GetOrdinal("IconUrl");
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

            Categories dto = new Categories();
            //load the data 
            if (!reader.IsDBNull(_ordinalCategoryId)) { dto.CategoryId = reader.GetInt32(_ordinalCategoryId); }
            if (!reader.IsDBNull(_ordinalCategory)) { dto.Category = reader.GetString(_ordinalCategory); }
            if (!reader.IsDBNull(_ordinalIconUrl)) { dto.IconUrl = reader.GetString(_ordinalIconUrl); }

            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
