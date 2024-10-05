import { HttpErrorResponse, HttpInterceptor, HttpInterceptorFn } from "@angular/common/http";
import { catchError, throwError } from "rxjs";

export const ErrorResponseInterceptor : HttpInterceptorFn = (req, next) =>
  next(req).pipe(catchError(handlerErrorReponse))


const handlerErrorReponse = (error: HttpErrorResponse) =>
{
  const errorResponse = `Error Status ${error.status}, message:${error.message}`;
  return throwError( () => errorResponse)
}
