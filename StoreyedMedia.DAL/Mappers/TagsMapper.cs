using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class TagsMapper : IDataMapper
    {

        #region Variables

        private int _ordinalTagId;
        private int _ordinalTag;
        private int _ordinalTagType;
        private int _ordinalSearchId;
        private int _ordinalCategoryId;
        private int _ordinalCategory;

        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {
            _ordinalTagId = ColumnExists(reader, "TagId") ? reader.GetOrdinal("TagId") : CommonBase.NonExistantOrdinal;

            _ordinalTag = ColumnExists(reader, "Tag") ? reader.GetOrdinal("Tag") : CommonBase.NonExistantOrdinal;

            _ordinalTagType = ColumnExists(reader, "TagType") ? reader.GetOrdinal("TagType") : CommonBase.NonExistantOrdinal;

            _ordinalCategory = ColumnExists(reader, "Category") ? reader.GetOrdinal("Category") : CommonBase.NonExistantOrdinal;

            _ordinalCategoryId = ColumnExists(reader, "CategoryId") ? reader.GetOrdinal("CategoryId") : CommonBase.NonExistantOrdinal;
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

            Tags dto = new Tags();
            //load the data 
            if (CommonBase.NonExistantOrdinal != _ordinalTagId && !reader.IsDBNull(_ordinalTagId)) { dto.TagId = reader.GetInt32(_ordinalTagId); }

            if (CommonBase.NonExistantOrdinal != _ordinalTag && !reader.IsDBNull(_ordinalTag)) { dto.Tag = reader.GetString(_ordinalTag); }

            if (CommonBase.NonExistantOrdinal != _ordinalTagType && !reader.IsDBNull(_ordinalTagType)) { dto.TagType = reader.GetString(_ordinalTagType); }

            if (CommonBase.NonExistantOrdinal != _ordinalCategory && !reader.IsDBNull(_ordinalCategory)) { dto.Category = reader.GetString(_ordinalCategory); }

            if (CommonBase.NonExistantOrdinal != _ordinalCategoryId && !reader.IsDBNull(_ordinalCategoryId)) { dto.CategoryId = reader.GetInt32(_ordinalCategoryId); }

            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
