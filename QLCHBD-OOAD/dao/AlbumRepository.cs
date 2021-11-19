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
    }
}
