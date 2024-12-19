using System.Text.Json.Serialization;


public class EventModel
{
    // {
    //     "EventName": 1,
    //     "EventDate": "0",
    //     "EventTime": "xyz",
    //     "Description": "richard",
    //     "RemainingSlots": "0001-01-01T00:00:00",
    //     "MaxCapacity": "36849255",
    //     "attendees": {
    //         "Name": "richard",
    //         "Email": "richard@gmail.com",
    //         "PeopleToAttend": 8,
    //         "Cost": 100
    //     }
    // }
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("EventName")]
    public string EventName { get; set; }

    [JsonPropertyName("EventDate")]
    public DateTime EventDate { get; set; }

    [JsonPropertyName("EventTime")]
    public string EventTime { get; set; }

    [JsonPropertyName("Description")]
    public string Description { get; set; }

    [JsonPropertyName("RemainingSlots")]
    public int RemainingSlots { get; set; }

    [JsonPropertyName("MaxCapacity")]
    public int MaxCapacity { get; set; }

    [JsonPropertyName("attendees")]
    //  list will be made even if no allergies are given.
    public AttendeesModel attendees { get; set; }

    // tbh idk why this needs to be here, but it does. it doesnt work without.
    // public EventModel() { }


    public EventModel(int ID, string eventName, DateTime eventDate, string eventTime, string description, int remainingSlots, int maxCapacity, AttendeesModel attendees)
    {
        this.Id = ID;
        this.EventName = eventName;
        this.EventDate = eventDate;
        this.EventTime = eventTime;
        this.Description = description;
        this.RemainingSlots = remainingSlots;
        this.MaxCapacity = maxCapacity;
        this.attendees = attendees;
    }
}