namespace Balta.NotificationContext
{
    public abstract class Notifiable
    {

        public List<Notification> Notifications { get; set; }
        public Notifiable()
        {
            Notifications = new List<Notification>();
        }
        public void AddNotification(Notification notification)
        {
            Notifications.Add(notification);
        }
        public void AddNotifications(IEnumerable<Notification> notifications)
        {
            Notifications.AddRange(notifications);
        }
        //nao tem setter pode usar 'arrow'
        public bool IsInvalid => Notifications.Any();
    }
}