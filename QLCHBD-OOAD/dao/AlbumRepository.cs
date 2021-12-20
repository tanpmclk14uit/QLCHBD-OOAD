using QLCHBD_OOAD.model.album;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QLCHBD_OOAD.dao
{
    class AlbumRepository
    {
        private Db db;
        public static AlbumRepository instance;

        public static AlbumRepository getInstance()
        {
            if (instance == null)
            {
                instance = new AlbumRepository();
                return instance;
            }
            return instance;
        }

        public AlbumRepository()
        {
            db = Db.getInstace();
        }
        public void addAlbum(string name)
        {
            string command = "INSERT INTO album (`name`) VALUES ('" + name + "');";
            var reader = db.executeCommand(command);
            db.closeConnection();
        }

        public ObservableCollection<Album> getAllAlbum()
        {
            ObservableCollection<Album> albums = new ObservableCollection<Album>();
            string commnad = "SELECT * FROM `album`";
            var reader = db.executeCommand(commnad);
            while (reader.Read())
            {
                if (reader[2] != reader[3])
                {
                    
                        Album album = new Album((long)reader[0], reader[1].ToString(), (DateTime)reader[2], (DateTime)reader[3]);
                        albums.Add(album);                 
                }
                else
                {
                    Album album = new Album((long)reader[0], reader[1].ToString(), (DateTime)reader[2]);
                    albums.Add(album);
                }
            }
            db.closeConnection();
            return albums;
        }

        public long getAlbumIdByName(string name)
        {
            long result = 0;
            string command = $"SELECT album.id FROM album WHERE album.name = '{name}'";
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                result = (long)reader[0];
            }
            return result;
        }

        public string getAlbumNameById(long id)
        {
            string result = "";
            string command = $"SELECT album.name FROM album WHERE album.id = '{id}'";
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                result = (string)reader[0];
            }
            return result;
        }
    }
}
