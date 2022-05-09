import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { IShipOffering } from "./ship-offering";
import { ShipOfferingService } from "./ship-offering.service";

@Component({
    templateUrl: './ship-offering-detail.component.html',
    styleUrls: ['./ship-offering-detail.component.css']
})
export class ShipOfferingDetailComponent implements OnInit {
    errorMessage: string = '';
    offering: IShipOffering | undefined;

    constructor(private route: ActivatedRoute,
                private router: Router,
                private shipOfferingService: ShipOfferingService) { }

    ngOnInit(): void {
        const id = Number(this.route.snapshot.paramMap.get('id'));
        if (id) {
            this.getShipOffering(id);
        }
    }

    getShipOffering(id: number): void {
        this.shipOfferingService.getShipOfferig(id).subscribe({
            next: offering => this.offering = offering,
            error: err => this.errorMessage = err
        });
    }
}