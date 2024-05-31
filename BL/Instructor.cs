namespace Excersice1.BL
{
    public class Instructor
    {
        int id;
        string title;
        string name;
        string image;
        string jobTitle;
        static List<Instructor> instructorsList = new List<Instructor>();
        private static readonly SemaphoreSlim listLock = new SemaphoreSlim(1, 1);

        public Instructor() { }

        public Instructor(int id, string title, string name, string image, string jobTitle)
        {
            Id = id;
            Title = title;
            Name = name;
            Image = image;
            JobTitle = jobTitle;
        }

        public async Task<bool> Insert()
        {
            await listLock.WaitAsync();
            try
            {
                if (instructorsList.Any(instructor => instructor.id == this.id))
                {
                    return false; // Instructor already exists
                }

                instructorsList.Add(this);
                return true; // Instructor added successfully
            }
            finally
            {
                listLock.Release();
            }
        }

        public static async Task<List<Instructor>> Read()
        {
            await listLock.WaitAsync();
            try
            {
                return new List<Instructor>(instructorsList); // Return a copy to avoid modification during enumeration
            }
            finally
            {
                listLock.Release();
            }
        }

        public static bool getInstructorById(int id)
        {
            foreach (Instructor instructor in instructorsList)
            {
                if (instructor.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

        public int Id { get => id; set => id = value; }
        public string Title { get => title; set => title = value; }
        public string Name { get => name; set => name = value; }
        public string Image { get => image; set => image = value; }
        public string JobTitle { get => jobTitle; set => jobTitle = value; }
    }
}
