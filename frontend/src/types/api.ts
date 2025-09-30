export interface EchoRequest {
  text: string;
}

export interface EchoResponse {
  text: string;
}

export interface ApiError {
  errors: string[];
}