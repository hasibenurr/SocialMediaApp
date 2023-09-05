import { Post } from "./post.model";

export class User {
	constructor(){
		this.id = '';
		this.name = '';
		this.surname = '';
		this.username = '';
		this.email = '';
		this.password = '';
		this.posts;
	}
	public id;
	public name;
	public surname;
	public username;
	public email;
	public password;
	public posts: Post[] = [];
}