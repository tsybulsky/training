using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Linq;
using Notes.Common.Exceptions;

namespace Notes.NoteMappers
{
    public class NotesDbMapper : INotesDbMapper
    {
        private readonly List<DbMapItem> _mapItems = new List<DbMapItem>();
        private readonly Type _creationType;
        public NotesDbMapper(Type type, DataTable datatable)
        {
            _creationType = type;
            PropertyInfo[] properties = type.GetProperties();
            for (int i = 0; i < datatable.Rows.Count;i++)
            {
                try
                {
                    DataRow row = datatable.Rows[i];
                    string fieldName = row.Field<string>("ColumnName").ToLower();
                    PropertyInfo property = properties.First(p => p.Name.ToLower() == fieldName);
                    if (property != null)
                    {                        
                        _mapItems.Add(new DbMapItem() { ModelProperty = property, ColumnIndex = row.Field<int>("ColumnOrdinal") });
                    }
                }
                catch
                {

                }
            }
        }
        public T Map<T>(IDataReader reader) where T: class, new()
        {
            try
            {
                if (!typeof(T).Equals(_creationType))
                    return null;
                T returnValue = new T();
                foreach (DbMapItem item in _mapItems)
                {
                    if (reader[item.ColumnIndex] is System.DBNull)
                    {
                        item.ModelProperty.SetValue(returnValue, null);
                    }
                    else
                    {
                        item.ModelProperty.SetValue(returnValue, reader[item.ColumnIndex]);
                    }
                }
                return returnValue;
            }
            catch (Exception e)
            {
                throw new NoteDatabaseException(e.Message);
            }
        }

    }
}
