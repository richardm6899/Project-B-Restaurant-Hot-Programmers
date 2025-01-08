using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

public class EventLogic
{
    public List<EventModel> event_list;
    static private EventAccess eventAccess = new();
    public EventLogic()
    {
        event_list = eventAccess.LoadAll();
    }

    //  CreateEvents() where the admin must be able to create an event and be added to the events list (JSON).
    //  CheckAvailability() timeslot to check if restaurant is available for certain times, 
    // should be able to choose 2 timeslots or wholeday.
    public void UpdateEvent(EventModel a_event)
    {
        //Find if there is already an model with the same id
        int index = event_list.FindIndex(s => s.Id == a_event.Id);

        if (index != -1)
        {
            //update existing model
            event_list[index] = a_event;
        }
        else
        {
            //add new model
            event_list.Add(a_event);
        }
        eventAccess.WriteAll(event_list);

    }

    public EventModel Create_event(int ID, string EventName, DateTime EventDate, string EventTime, string description, int remainingSlots, int maxCapacity, AttendeesModel attendees)//tested
    {
        int new_id = event_list.Count + 1;

        EventModel _event = new(new_id, EventName, EventDate, EventTime, description, remainingSlots, maxCapacity, attendees);

        // add created event to event list
        event_list.Add(_event);
        eventAccess.WriteAll(event_list);
        return _event;

    }

    public bool CheckAvailability(ReservationLogic reservation_list, DateTime date, string timeslot)
    {
        foreach (var looped_event in event_list)
        {
                if(looped_event.EventDate == date || looped_event.EventTime.Contains(timeslot))
                {
                    return false; // meaning that 
                }
        }
        foreach (var looped_reservation in reservation_list._reservations)
        {
            if (looped_reservation.Date == date || looped_reservation.TimeSlot.Contains(timeslot))
            {
                return false; // 
            }
        }
        return true;
    }

    //  CheckSignups() to check if clients have signed up for an event.
    // public bool CheckSignups(AccountsLogic account)
    // {

    // }
}