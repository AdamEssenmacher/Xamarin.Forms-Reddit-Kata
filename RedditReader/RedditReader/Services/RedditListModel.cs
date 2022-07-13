using System.Collections.Generic;

namespace RedditReader.Services
{
    // Sample data from: https://www.reddit.com/r/catpictures/.json
    internal class RedditListModel
    {
        public ModelData? Data { get; set; }

        public class ModelData
        {
            public IList<Child>? Children { get; set; }

            public class Child
            {
                public ItemData? Data { get; set; }

                public class ItemData
                {
                    public string? SelfText { get; set; }
                    public string? Title { get; set; }
                    public string? Thumbnail { get; set; }
                    public PreviewData? Preview { get; set; }

                    public class PreviewData
                    {
                        public IList<ImageData>? Images { get; set; }

                        public class ImageData
                        {
                            public SourceData? Source { get; set; }

                            public class SourceData
                            {
                                public string? Url { get; set; }
                            }
                        }
                    }
                }
            }
        }
    }
}