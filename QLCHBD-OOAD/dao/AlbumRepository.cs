using QLCHBD_OOAD.model.album;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ObservableCollection<Album> getAllAlbum()
        {
            ObservableCollection<Album> albums = new ObservableCollection<Album>();
            string commnad = "SELECT * FROM `disk`";
            var reader = db.executeCommand(commnad);
            while (reader.Read())
            {
                Album album;
                if (reader[2] != reader[3])
                {
                    album = new Album((long)reader[0], (string)reader[1], (DateTime)reader[2], (DateTime)reader[3]);
                }
                else
                {
                    album = new Album((long)reader[0], (string)reader[1], (DateTime)reader[2]);
                }    
                albums.Add(album);
            }
            db.closeConnection();
            return albums;
        }
    }
}
