
using ProfileClasses;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ClassLibrary;

public partial class Advertisement : IUsingUUID
{
    private Guid _id;
    private string mealName="";
    private DateTime pickupTimeSpan;
    private Cuisines[] _tags = {Cuisines.None};
    private DateTime timeOfMaking;
    private Distributor _distributor;
    private Profile? _buyer;
    private double _cost;

    public virtual double Cost { get { return _cost; } set { _cost = value; } }
    public virtual Profile? Buyer { get { return _buyer; } set { _buyer = value; } }
    public virtual Distributor Distributor { get { return _distributor; } set { _distributor = value; } } 
    public string MealName { get { return mealName; } set { mealName = value; } }
    public DateTime PickupTimeSpan { get { return pickupTimeSpan; } set { pickupTimeSpan = value; } }
    public string Tags { get { return JsonSerializer.Serialize(_tags); } set { _tags = JsonSerializer.Deserialize<Cuisines[]>(value); } }
    public DateTime TimeOfMaking { get { return timeOfMaking; } set { timeOfMaking = value; } }

    public Guid Id { get { return _id; } set { _id = value; } }

    [NotMapped]
    public Cuisines[] TagsArray { get => _tags; set => _tags = value; }
}
