using System.ComponentModel.DataAnnotations;

namespace MainActivity.Models {
    public sealed class ArticleContent {
        [Key]
        public int ID { get; set; }

        [DataType(DataType.Text)]
        public string Title { get; set; }

        [DataType(DataType.Text)]
        public string ParagraphTitle { get; set; }

        [DataType(DataType.Text)]
        public string ParagraphContent { get; set; }

    }// Class ENDS 
}
