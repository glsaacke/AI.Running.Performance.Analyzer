namespace api.Repositories
{
    public class ActivityRepository
    {
        public static List<Movie> GetAllUsers(){
            List<User> users = new List<User>();

            string cs = Connection.conString;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = "SELECT userId, email, password, fName, lName, age, weight FROM Users";

            using var cmd = new MySqlCommand(stm, con);

            using var reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            foreach (DataRow row in dataTable.Rows)
            {
                User user = new User
                {
                    userId = convert.ToInt32(row["userId"]),
                    email = row["email"].ToString(),
                    password = row["password"].ToString(),
                    fName = row["fName"].ToString(),
                    lName = row["lName"].ToString(),
                    age = Convert.ToInt32(row["age"]),
                    weight = Convert.ToInt32(row["weight"])
                };
                users.Add(user);
            }

            return users;
        }
         public static void CreateUser(User user){
            string cs = Connection.conString;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO Activites(userId, email, password, fName, lName, age, weight) VALUES(@userId, @email, @password, @fName, @lName, @age, @weight)";

            using var cmd = new MySqlCommand(stm,con);

            cmd.Parameters.AddWithValue("@userId", user.userId);
            cmd.Parameters.AddWithValue("@email", user.email);
            cmd.Parameters.AddWithValue("@password", user.Rating);
            cmd.Parameters.AddWithValue("@fName", user.fName);
            cmd.Parameters.AddWithValue("@lName", user.lName);
            cmd.Parameters.AddWithValue("@age", user.age);
            cmd.Parameters.AddWithValue("@weight", user.weight);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public static void UpdateActivity(User user){ //TODO build user Edit

        }
        public static void DeleteActivity(User user){ //TODO build user Delete
            string cs = Connection.conString;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE SET deleted = 1 WHERE id = @movieId;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@userId", user.userId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}