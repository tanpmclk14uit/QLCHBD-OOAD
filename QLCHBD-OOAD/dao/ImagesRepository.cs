using QLCHBD_OOAD.appUtil;
using QLCHBD_OOAD.model.images;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public List<Images> getAllImages()
        {
            List<Images> images = new List<Images>();
            string commnad = "SELECT * FROM `disk`";
            var reader = db.executeCommand(commnad);
            while(reader.Read())
            {

                    Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (Boolean)reader[6], (int)reader[7], (long)reader[8],
                        (long)reader[9], (int)reader[10], (DateTime)reader[16], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14], (int)reader[15]);
                    images.Add(image);

                
            }
            db.closeConnection();
            return images;
        }
        public ObservableCollection<Images> getAllImagesForRental()
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string command = "Select id, name, image, quantity, rented from disk";
            var reader = db.executeCommand(command);
            while(reader!= null && reader.Read())
            {
                images.Add(new Images((long)reader[0], (string)reader[1], (string)reader[2], (int)reader[3], (int)reader[4]));
            }
            return images;
        }
        public string getProviderNameById(long id)
        {
            string providerName = null;
            string command = "SELECT provider.name FROM provider INNER JOIN disk WHERE disk.provider = provider.id && disk.id = " + id;
            var reader = db.executeCommand(command);
            if(reader != null && reader.Read())
            {
                providerName = reader[0].ToString();
            }
            return providerName;
        }
        public int getPriceById(long id)
        {
            int price = 0;
            string command = "SELECT rental_price FROM disk WHERE disk.id = " + id;
            var reader = db.executeCommand(command);
            if (reader != null && reader.Read())
            {
                price = (int)reader[0];
            }
            return price;
        }
        public ObservableCollection<Images> getAllImagesForRentalLikeKeywordAndMatchId(string key)
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string command = "Select id, name, image, quantity, rented from disk where name like '%" + key +"%'";
            var reader = db.executeCommand(command);
            while (reader != null && reader.Read())
            {
                images.Add(new Images((long)reader[0], (string)reader[1], (string)reader[2], (int)reader[3], (int)reader[4]));
            }
            command = "Select id, name, image, quantity, rented from disk where id like '%" + key + "%'";
            reader = db.executeCommand(command);
            while (reader != null && reader.Read())
            {
                images.Add(new Images((long)reader[0], (string)reader[1], (string)reader[2], (int)reader[3], (int)reader[4]));
            }
            return images;
        }



        public List<Images> getImagesById(string id)
        {
            List<Images> images = new List<Images>();
            string command = "SELECT * FROM `disk` WHERE ID = " + id;
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Images image = new Images((Byte)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (Boolean)reader[6], (int)reader[7], (long)reader[8],
                   (long)reader[9], (int)reader[10], (DateTime)reader[16], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14], (int)reader[15]);
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
          
                Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (Boolean)reader[6], (int)reader[7], (long)reader[8],
                   (long)reader[9], (int)reader[10], (DateTime)reader[16], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14], (int)reader[15]);
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }

        public ObservableCollection<Images> getImagesByProviderID(string id)
        {
            ObservableCollection<Images> images = new ObservableCollection<Images>();
            string command = "SELECT * FROM `disk` WHERE provider = " + id;
            var reader = db.executeCommand(command);
            while (reader.Read())
            {

                Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (Boolean)reader[6], (int)reader[7], (long)reader[8],
                   (long)reader[9], (int)reader[10], (DateTime)reader[16], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14], (int)reader[15]);
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }
        public List<Images> getImagesByAlbum(long id)
        {
            List<Images> images = new List<Images>();
            string command = "SELECT disk.id, disk.name, disk.album, disk.quantity, disk.image, disk.locate, disk.checked, disk.rental_price, disk.provider, disk.id_by_provider, disk.loss_charges, disk.create_time, disk.update_time, disk.create_by, disk.update_by, rented, publishing_date FROM `disk` inner join `album` WHERE disk.album = album.id AND album.ID =" + id.ToString();
            var reader = db.executeCommand(command);
            while (reader.Read())
            {
                Images image = new Images((long)reader[0], (string)reader[1], (long)reader[2], (int)reader[3], (string)reader[4], (string)reader[5], (Boolean)reader[6], (int)reader[7], (long)reader[8],
                      (long)reader[9], (int)reader[10], (DateTime)reader[16], (DateTime)reader[11], (DateTime)reader[12], (long)reader[13], (long)reader[14], (int)reader[15]);
                images.Add(image);
            }
            db.closeConnection();
            return images;
        }

        public bool uploadNewImage(Images images)
        {
            int isCheck = 0;
            string format = "yyyy-MM-dd";
            if (images.isCheck == true) isCheck = 1;
            bool result = false;

            string command = $"INSERT INTO `disk` ( `name`, `album`, `quantity`, `image`, `locate`, `checked`, `rental_price`, `provider`, `id_by_provider`, `loss_charges`, `create_time`, `update_time`, `create_by`, `update_by`, `rented`, `publishing_date`) VALUES ('{images.name}','{images.idAlbum}','{images.quantity}','{images.image}','{images.locate}','{isCheck}','{images.rentalPrice}','{images.idProvider}','{images.idByProvider}','{images.lostCharges}', CURRENT_TIMESTAMP, CURRENT_TIMESTAMP, '{CurrentStaff.getInstance().currentStaff.id}', '{CurrentStaff.getInstance().currentStaff.id}', '0', '{images.publish.ToString(format)}')";
            var reader = db.executeCommand(command);
            if (reader != null)
            {
                result = true;
            }
            db.closeConnection();
            return result;
        }

        public bool deleteDisk(long id)
        {
            bool result = false;
            string command = $"DELETE FROM `disk` WHERE ID = {id}";
            var reader = db.executeCommand(command);
            if (reader != null)
            {
                result = true;
            }
            db.closeConnection();
            return result;
        }

        // for set ID
        public long getNumberOfImage()
        {
            string command = "Select count(*) from `disk`";
            var reader = db.executeCommand(command);
            long result = 0;
             while (reader.Read())
            {
                result = (long)reader[0];
            }
            db.closeConnection();
            return result;
        }

        //for get all amount of image
        public long getTotalOfImage()
        {
            string command = "Select Sum(disk.quantity) from `disk`";
            var reader = db.executeCommand(command);
            long result = 0;
            while (reader.Read())
            {
                if (reader[0] != DBNull.Value)
                    result = Convert.ToInt64((Decimal)reader[0]);
            }
            db.closeConnection();
            return result;
        }

        public bool updateImage(long id, string image, bool isCheck, string name, int quantity, long provider, long idByProvider, DateTime publish, int loss, int price, string locate, long album)
        {
            string format = "yyyy-MM-dd";
            bool result = false;
            string command = $"UPDATE `disk` SET disk.album= {album} , disk.checked={isCheck}, disk.name = '{name}', disk.quantity = {quantity}, disk.provider = {provider}, disk.image= '{image}', disk.locate = '{locate}',  disk.update_time= CURRENT_TIMESTAMP	, disk.loss_charges = {loss}, disk.rental_price= {price}, disk.id_by_provider = {idByProvider}, disk.publishing_date = '{publish.ToString(format)}' WHERE disk.id = {id}";
            var reader = db.executeCommand(command);
            if (reader != null)
            {
                result = true;
            }
            db.closeConnection();
            return result;

        }
        public void updateImage(Images images)
        {
            int quantity = 0;
            string command = "SELECT quantity FROM disk WHERE id = " + images.id;
            var reader = db.executeCommand(command);

            if (reader.Read())
            {
                quantity += (int)reader[0];

            }

            command = $"UPDATE `ooad_qlchbd`.`disk` SET `name` = '{images.name}', `album` = '{images.idAlbum}', `quantity` = '{images.quantity + quantity}', `image` = '{images.image}', `locate` = '{images.locate}', `rental_price` = '{images.rentalPrice}', `provider` = '{images.idProvider}', `id_by_provider` = '{images.idByProvider}', `loss_charges` = '{images.providerPrice}', `update_by` = '{images.updateBy}' WHERE (`id` = '{images.id}');";
            reader = db.executeCommand(command);
            db.closeConnection();
        }

        public bool imageIsNotNull(string id)
        {
            List<Images> images = new List<Images>();
            string command = "SELECT * FROM `disk` WHERE ID = " + id;
            var reader = db.executeCommand(command);
            if (reader.Read())
            {
                db.closeConnection();
                return true;
            }
            db.closeConnection();
            return false;
        }
    }                           
}                               