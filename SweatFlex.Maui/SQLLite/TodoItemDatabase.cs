using SQLite;
using SweatFlex.Maui.Models;

namespace SweatFlex.Maui.SQLLite
{
    public class TodoItemDatabase
    {
        SQLiteAsyncConnection Database;

        public TodoItemDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
                return;

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            var result = await Database.CreateTableAsync<TrainingExerciseLocal>();
        }

        public async Task<List<TrainingExerciseLocal>> GetItemsAsync()
        {
            await Init();
            return await Database.Table<TrainingExerciseLocal>().ToListAsync();
        }

        public async Task<List<TrainingExerciseLocal>> GetItemsNotDoneAsync()
        {
            await Init();
            return await Database.Table<TrainingExerciseLocal>().Where(t => t.Done).ToListAsync();

            // SQL queries are also possible
            //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }

        public async Task<TrainingExerciseLocal> GetItemAsync(int id)
        {
            await Init();
            return await Database.Table<TrainingExerciseLocal>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveItemAsync(TrainingExerciseLocal item)
        {
            await Init();
            if (item.Id != 0)
                return await Database.UpdateAsync(item);
            else
                return await Database.InsertAsync(item);
        }

        public async Task<int> DeleteItemAsync(TrainingExerciseLocal item)
        {
            await Init();
            return await Database.DeleteAsync(item);
        }
    }
}
