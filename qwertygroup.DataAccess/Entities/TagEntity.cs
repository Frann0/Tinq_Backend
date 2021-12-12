using qwertygroup.Core.Models;

namespace qwertygroup.DataAccess.Entities
{
    public class TagEntity
    {
        private TagEntity tag;
        public int Id { get; set; }
        public string Text { get; set; }

        public Tag ToTag()
        {
            return new Tag { Id = this.Id, Text = this.Text };
        }
    }
}