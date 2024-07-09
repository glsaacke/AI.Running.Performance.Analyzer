namespace api.Repositories
{
    public class ActivityRepository
    {
        public static List<Movie> GetAllActivities(){
            List<Activity> activities = new List<Activity>();

            string cs = Connection.conString; //FIXME constring utility
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = "SELECT activityId, name, type, distance, duration, pace, heartrate FROM Activities";

            using var cmd = new MySqlCommand(stm, con);

            using var reader = cmd.ExecuteReader();
            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            foreach (DataRow row in dataTable.Rows)
            {
                Activity activity = new activity
                {
                    activityId = convert.ToInt32(row["activityId"]),
                    name = row["name"].ToString(),
                    type = row["type"].ToString(),
                    distance = Convert.ToInt32(row["distance"]),
                    duration = Convert.ToInt32(row["duration"]),
                    pace = Convert.ToInt32(row["pace"]),
                    heartrate = Convert.ToInt32(row["heartrate"])
                };
                activities.Add(activity);
            }

            return activities;
        }
         public static void CreateActivity(Activity activity){
            string cs = Connection.conString;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"INSERT INTO Activites(activityId, name, type, distance, duration, pace, heartrate) VALUES(@activityId, @name, @type, @distance, @duration, @pace, @heartrate)";

            using var cmd = new MySqlCommand(stm,con);

            cmd.Parameters.AddWithValue("@activityId", activity.activityId);
            cmd.Parameters.AddWithValue("@name", activity.name);
            cmd.Parameters.AddWithValue("@type", activity.Rating);
            cmd.Parameters.AddWithValue("@distance", activity.distance);
            cmd.Parameters.AddWithValue("@duration", activity.duration);
            cmd.Parameters.AddWithValue("@pace", activity.pace);
            cmd.Parameters.AddWithValue("@heartrate", activity.heartrate);

            cmd.Prepare();

            cmd.ExecuteNonQuery();
        }

        public static void UpdateActivity(Activity activity){ //TODO build act. Edit

        }
        public static void DeleteActivity(Activity activity){ //TODO build act. Delete
            string cs = Connection.conString;
            using var con = new MySqlConnection(cs);
            con.Open();

            string stm = @"UPDATE SET deleted = 1 WHERE id = @movieId;";

            using var cmd = new MySqlCommand(stm, con);
            cmd.Parameters.AddWithValue("@activityId", activity.activityId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}