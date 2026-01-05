namespace backend.Models
{
    public enum TicketType
    {
        Student,
        ExStudent,
        Faculty,
        Guest,
        VIP,
        VVIP
    }

    public enum UserRole
    {
        SuperAdmin,
        Admin,
        Staff,
        Attendee
    }

    public enum TicketStatus
    {
        Active,
        Blocked,
        Expired
    }
    
    public enum ScanType
    {
        Entry,
        Food
    }

    public enum MealType
    {
        Breakfast,
        Lunch,
        EveningSnacks,
        Dinner
    }


}



