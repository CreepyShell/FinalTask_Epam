export interface postModel {
  id: string;
  userId: string;
  header: string;
  text: string;
  postTopic: string;
  commentIds: string[];
  reactionIds: string[];
  createdAt: Date;
  updatedAt: Date;
}
