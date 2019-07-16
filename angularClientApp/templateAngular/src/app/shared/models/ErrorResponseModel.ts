export class ErrorResponseModel {
  isOk: boolean;
  statusCode: number;
  statusText: string;
  apiUrl: string;
  httpMessage: string;
  coreMessage: string;

  public constructor(init?:Partial<ErrorResponseModel>) {
    Object.assign(this, init);
  }
}
