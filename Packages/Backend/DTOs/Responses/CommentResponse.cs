namespace Backend.DTOs.Responses
{
	public class CommentResponse
	{
		public UserBasicResponse User { get; set; }
		public string Content { get; set; }
		public int ChildCommentCount { get; set; }
		public int LikeCount { get; set; }
		public int ParentId { get; set; }
	}
}