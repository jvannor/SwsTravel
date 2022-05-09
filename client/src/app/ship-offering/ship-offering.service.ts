import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { catchError, Observable, tap, throwError } from "rxjs";
import { IShipOffering } from "./ship-offering";

@Injectable({
    providedIn: 'root'
})
export class ShipOfferingService {
    private shipOfferingServiceUrl = 'api/ship-offering';

    constructor(private http: HttpClient) { }

    getShipOfferings() : Observable<IShipOffering[]> {
        return this.http.get<IShipOffering[]>(this.shipOfferingServiceUrl).pipe(
            tap(data => console.log('All', JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    getShipOfferig(id: number): Observable<IShipOffering | undefined> {
        return this.http.get<IShipOffering>(this.shipOfferingServiceUrl + `/${id}`).pipe(
            tap(data => console.log("Offering", JSON.stringify(data))),
            catchError(this.handleError)
        );
    }

    private handleError(err: HttpErrorResponse) {
        let errorMessage = "";
        if (err.error instanceof(ErrorEvent)) {
            errorMessage = `An error occurred: ${err.error.message}`;
        } else {
            errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        console.log(errorMessage);
        return throwError(() => new Error(errorMessage));
    }
}