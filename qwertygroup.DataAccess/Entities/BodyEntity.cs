using qwertygroup.Core.Models;

namespace qwertygroup.DataAccess.Entities
{
    public class BodyEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Body ToBody()
        {
            return new Body { Id = this.Id, Text = this.Text };
        }
    }
}