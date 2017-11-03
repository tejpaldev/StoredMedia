using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class StoryListMapper : IDataMapper
    {
        #region Variables



        private int _ordinalStoryListId;
        private int _ordinalId;
        private int _ordinalStoryId;
        private int _ordinalListType;
        private int _ordinalStoryStatus;
        private int _ordinalContactId;
        private int _ordinalFirstName;
        private int _ordinalLastName; 
        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {

            _ordinalStoryListId = ColumnExists(reader, "StoryListId") ? reader.GetOrdinal("StoryListId") : CommonBase.NonExistantOrdinal;
            _ordinalId = ColumnExists(reader, "Id") ? reader.GetOrdinal("Id") : CommonBase.NonExistantOrdinal;
            _ordinalStoryId = ColumnExists(reader, "StoryId") ? reader.GetOrdinal("StoryId") : CommonBase.NonExistantOrdinal;
            _ordinalListType = ColumnExists(reader, "ListType") ? reader.GetOrdinal("ListType") : CommonBase.NonExistantOrdinal;
            _ordinalStoryStatus = ColumnExists(reader, "StoryStatus") ? reader.GetOrdinal("StoryStatus") : CommonBase.NonExistantOrdinal;
            _ordinalContactId = ColumnExists(reader, "ContactId") ? reader.GetOrdinal("ContactId") : CommonBase.NonExistantOrdinal;
            _ordinalFirstName = ColumnExists(reader, "FirstName") ? reader.GetOrdinal("FirstName") : CommonBase.NonExistantOrdinal;
            _ordinalLastName = ColumnExists(reader, "LastName") ? reader.GetOrdinal("LastName") : CommonBase.NonExistantOrdinal;




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

            StoryList dto = new StoryList();
            //load the data  



            if (CommonBase.NonExistantOrdinal != _ordinalStoryListId && !reader.IsDBNull(_ordinalStoryListId)) { dto.StoryListId = reader.GetInt32(_ordinalStoryListId); }
            if (CommonBase.NonExistantOrdinal != _ordinalId && !reader.IsDBNull(_ordinalId)) { dto.Id = reader.GetInt32(_ordinalId); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryId && !reader.IsDBNull(_ordinalStoryId)) { dto.StoryId = reader.GetInt32(_ordinalStoryId); }
            if (CommonBase.NonExistantOrdinal != _ordinalListType && !reader.IsDBNull(_ordinalListType)) { dto.ListType = reader.GetString(_ordinalListType); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryStatus && !reader.IsDBNull(_ordinalStoryStatus)) { dto.StoryStatus = reader.GetString(_ordinalStoryStatus); }
            if (CommonBase.NonExistantOrdinal != _ordinalContactId && !reader.IsDBNull(_ordinalContactId)) { dto.ContactId = reader.GetInt32(_ordinalContactId); }
            if (CommonBase.NonExistantOrdinal != _ordinalFirstName && !reader.IsDBNull(_ordinalFirstName)) { dto.FirstName = reader.GetString(_ordinalFirstName); }
            if (CommonBase.NonExistantOrdinal != _ordinalLastName && !reader.IsDBNull(_ordinalLastName)) { dto.LastName = reader.GetString(_ordinalLastName); }


            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
