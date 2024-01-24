using LibraryProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LibraryProject.Controllers
{
    public class BooksController : Controller
    {
        public IConfiguration Configuration { get; }
        public BooksController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // GET: BooksController
        public readonly string connectionString = "Data Source=ICPU0076\\SQLEXPRESS;Initial Catalog=Library1;Persist Security Info=False;User ID=sa;Password=sql@123;Pooling=False;Multiple Active Result Sets=False;Encrypt=False;Trust Server Certificate=False;Command Timeout=0";

        public ActionResult Index()
        {
            List<Book> books = new List<Book>();
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("SELECT * FROM book", con))
                {
                    con.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        books = new List<Book>();
                        while (reader.Read())
                        {
                            var book = new Book
                            {
                                Bookid = Convert.ToInt32(reader["Bookid"]),
                                BookName = reader["BookName"].ToString(),
                                BookDescription = reader["BookDescription"].ToString(),
                                Author = reader["Author"].ToString(),
                                Price = Convert.ToInt32(reader["Price"]),
                                Published_Date = Convert.ToDateTime(reader["Published_Date"])
                            };
                            books.Add(book);
                        }
                    }
                }
            }

            return View(books);

        }

        // GET: BooksController/Details/5

        public ActionResult Details(int id)
        {
            Book obj = new Book();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = $"select * from Book where Bookid = {id}";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            obj.Bookid = Convert.ToInt32(sdr["Bookid"]);
                            obj.BookName = Convert.ToString(sdr["BookName"]);
                            obj.BookDescription = Convert.ToString(sdr["BookDescription"]);
                            obj.Author = Convert.ToString(sdr["Author"]);
                            obj.Price = Convert.ToInt32(sdr["Price"]);
                            obj.Published_Date = Convert.ToDateTime(sdr["Published_Date"]);

                        }

                        con.Close();

                    }

                }
            }
            return View(obj);
        }


        // GET: BooksController/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: BooksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Book model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                using (var cmd = new SqlCommand("insertbook", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookName", model.BookName);
                    cmd.Parameters.AddWithValue("@BookDescription", model.BookDescription);
                    cmd.Parameters.AddWithValue("@Author", model.Author);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@Published_Date", model.Published_Date);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            return RedirectToAction("Index");
        }

        // GET: BooksController/Edit/5
        public ActionResult Edit(int id)
        {
            Book obj = new Book();
            string connectionString = Configuration["ConnectionStrings:Default"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = $"select * from Book where Bookid='{id}'";

                SqlCommand cmd = new SqlCommand(sql, connection);
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        obj.Bookid = Convert.ToInt32(sdr["Bookid"]);
                        obj.BookName = Convert.ToString(sdr["BookName"]);
                        obj.BookDescription = Convert.ToString(sdr["BookDescription"]);
                        obj.Author = Convert.ToString(sdr["Author"]);
                        obj.Price = Convert.ToInt32(sdr["Price"]);
                        obj.Published_Date = Convert.ToDateTime(sdr["Published_Date"]);
                    }
                    connection.Close();
                }

            }
            return View(obj);
        }

        // POST: BooksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Book book, int id)
        {
            string connectionString = Configuration["ConnectionStrings:Default"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Update Book SET BookName='{book.BookName}',BookDescription='{book.BookDescription}',Author='{book.Author}',Price='{book.Price}',Published_Date='{book.Published_Date}' where Bookid='{book.Bookid}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            return RedirectToAction("Index");
        }

        // GET: BooksController/Delete/5
        public ActionResult Delete(int id)
        {
            Book obj = new Book();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sqlQuery = $"select * from Book where Bookid = {id}";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                {

                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {

                        while (sdr.Read())
                        {
                            obj.Bookid = Convert.ToInt32(sdr["Bookid"]);
                            obj.BookName = Convert.ToString(sdr["BookName"]);
                            obj.BookDescription = Convert.ToString(sdr["BookDescription"]);
                            obj.Author = Convert.ToString(sdr["Author"]);
                            obj.Price = Convert.ToInt32(sdr["Price"]);
                            obj.Published_Date = Convert.ToDateTime(sdr["Published_Date"]);
                        }

                        con.Close();
                    }
                }
            }
            return View(obj);
        }

        // POST: BooksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Book book)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sql = $"Delete from Book where Bookid='{id}'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {


                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
                return RedirectToAction("Index");
            }
        }


    }
}

