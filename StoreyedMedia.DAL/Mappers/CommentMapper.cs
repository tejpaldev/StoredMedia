using StoreyedMedia.Model;
using System;
using System.Data;

namespace StoreyedMedia.DAL.Mappers
{
    public class CommentMapper : IDataMapper
    {
        #region Variables



        private int _ordinalCommentId;
        private int _ordinalStoryId;
        private int _ordinalCommentDesc;
        private int _ordinalCreatedBy;
        private int _ordinalCreatedOnDateTime;

        private bool _isInitialized = false;

        #endregion

        private void InitializeMapper(IDataReader reader)
        {
            PopulateOrdinals(reader);
            _isInitialized = true;
        }

        public void PopulateOrdinals(IDataReader reader)
        {
            _ordinalCommentId = ColumnExists(reader, "Id") ? reader.GetOrdinal("Id") : CommonBase.NonExistantOrdinal;
            _ordinalStoryId = ColumnExists(reader, "StoryId") ? reader.GetOrdinal("StoryId") : CommonBase.NonExistantOrdinal;
            _ordinalCommentDesc = ColumnExists(reader, "Description") ? reader.GetOrdinal("Description") : CommonBase.NonExistantOrdinal;
            _ordinalCreatedBy = ColumnExists(reader, "CreatedBy") ? reader.GetOrdinal("CreatedBy") : CommonBase.NonExistantOrdinal;
            _ordinalCreatedOnDateTime = ColumnExists(reader, "CreatedOnDateTime") ? reader.GetOrdinal("CreatedOnDateTime") : CommonBase.NonExistantOrdinal;
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

            Comment dto = new Comment();
            //load the data  

            if (CommonBase.NonExistantOrdinal != _ordinalCommentId && !reader.IsDBNull(_ordinalCommentId)) { dto.CommentId = reader.GetInt32(_ordinalCommentId); }
            if (CommonBase.NonExistantOrdinal != _ordinalStoryId && !reader.IsDBNull(_ordinalStoryId)) { dto.StoryId = reader.GetInt32(_ordinalStoryId); }
            if (CommonBase.NonExistantOrdinal != _ordinalCommentDesc && !reader.IsDBNull(_ordinalCommentDesc)) { dto.CommentDesc = reader.GetString(_ordinalCommentDesc); }
            if (CommonBase.NonExistantOrdinal != _ordinalCreatedBy && !reader.IsDBNull(_ordinalCreatedBy)) { dto.CreatedBy = reader.GetString(_ordinalCreatedBy); }
            if (CommonBase.NonExistantOrdinal != _ordinalCreatedOnDateTime && !reader.IsDBNull(_ordinalCreatedOnDateTime)) { dto.CreatedOnDateTime = reader.GetDateTime(_ordinalCreatedOnDateTime); }
            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }
}
