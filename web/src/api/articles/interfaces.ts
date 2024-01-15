export interface CreateArticleCommand {
  title: string;
  content: string;
  creatorId: string;
}

export interface UpdateArticleCommand {
  id: string;
  title: string;
  content: string;
}

export interface CreateCommentCommand {
  text: string;
  commentatorId: string;
}

export interface ArticleDetailDto {
  id: string;
  title: string;
  content: string;
  creator: UserDto;
  comments?: CommentDto[];
  created: Date;
  updated: Date;
}

export interface ArticleDto {
  id: string;
  title: string;
  creator: string;
  created: Date;
  updated: Date;
}

export interface UserDetailDto extends UserDto {
  articles?: ArticleDto[];
}

export interface UserDto {
  id: string;
  fullName: string;
}

export interface CommentDto {
  id: string;
  text: string;
  commenator: string;
}
