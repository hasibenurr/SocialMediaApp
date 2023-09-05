export class Token {
	constructor(){
		this.accessToken = '';
		this.refreshToken = '';
		this.expiration = 0;
	}
	public accessToken;
	public refreshToken;
	public expiration;
}