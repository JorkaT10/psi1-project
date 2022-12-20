using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using ClassLibrary;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProfileClasses
{
    public class Account : IUsingUUID
    {
        private string _userName;
        private string _password;
        private Guid _id;
        public Guid Id { get { return _id; } set { _id = value; } }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
	public class Profile : IUsingUUID
    {
        private Guid _id;
        private string _email;
        private string? _phoneNumber;

        private string _name;
        private Address _address;
        private Cuisines[] _cuisines;
        private ObservableCollection<Distributor> _subscriptions;
        private ObservableCollection<Advertisement> _reservations;

        public virtual ObservableCollection<Advertisement>? Reservations { get { return _reservations; } set { _reservations = value; } }
        public virtual ObservableCollection<Distributor>? Subscriptions { get { return _subscriptions; } set { _subscriptions = value; } }
        public string Email { get { return _email; } set { _email = value; } }
        public string? PhoneNumber { get { return _phoneNumber; } set { _phoneNumber = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public string Cuisines { get { return JsonConvert.SerializeObject(_cuisines); } set { _cuisines = JsonConvert.DeserializeObject<Cuisines[]>(value); } }

        public Guid Id { get { return _id; } set { _id = value; } }
        public string Address { get { return JsonConvert.SerializeObject(_address); } set { _address = JsonConvert.DeserializeObject<Address>(value); } }

        [NotMapped]
        public Address TypedAddress { get { return _address; } set { _address = value; } }
        [NotMapped]
        public Cuisines[] CuisineArray { get { return _cuisines; } set { _cuisines = value; } }

    }
	public class Distributor : IUsingUUID
    {
        private double _rating;
        private int _ratingAmount;
        private Guid _id;
        private ObservableCollection<Advertisement>? _advertisements;
        private ObservableCollection<Profile>? _subscribers;

        public ObservableCollection<Profile>? Subscribers { get { return _subscribers; } set { _subscribers = value; } }
        public double Rating { get { return _rating; } set { _rating = value; } }
        public int RatingAmount { get { return _ratingAmount; } set { _ratingAmount = value; } }
        public Guid Id { get { return _id; } set { _id = value; } }
        public virtual ObservableCollection<Advertisement>? Advertisements { get { return _advertisements; } set { _advertisements = value; } }
    }

}