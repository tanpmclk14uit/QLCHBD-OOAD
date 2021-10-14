using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCHBD_OOAD.dao
{
    class ImagesRepository
    {
        private Db db;
        private static ImagesRepository instance;

        public static ImagesRepository getInstance()
        {
            if (instance != null)
            {
                return instance;
            }
            instance =  new ImagesRepository();
            return instance;
        }

        public ImagesRepository()
        {
            db = Db.getInstace();
        }

        public ObservableCollection<Images> getAllImages()
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string commnad = "SELECT * FROM `disk`";
            var reader = db.executeCommand(commnad);
            while(reader.Read())
            {
                Images image;
                if (reader[11] == reader[12])
                {
                    image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (int)reader[6], (int)reader[7], (long)reader[8],
                        (long)reader[9], (int)reader[10], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14]);
                }
                else
                {
                    image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (int)reader[6], (int)reader[7], (long)reader[8],
                        (long)reader[9], (int)reader[10], (DateTime)reader[11], (long)reader[13]);
                }    
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }

        public ObservableCollection<Images> getImagesById(string id)
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string command = "SELECT * FROM `disk` WHERE ID = " + id;
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (int)reader[6], (int)reader[7], (long)reader[8],
                   (long)reader[9], (int)reader[10], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14]);
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }

        public ObservableCollection<Images> getImagesByName(string name)
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string command = "SELECT * FROM `disk` WHERE NAME = " + name;
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (int)reader[6], (int)reader[7], (long)reader[8],
                   (long)reader[9], (int)reader[10], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14]);
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }
    }
}
