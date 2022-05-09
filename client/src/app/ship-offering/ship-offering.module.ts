import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";
import { ShipOfferingDetailComponent } from "./ship-offering-detail.component";
import { ShipOfferingComponent } from "./ship-offering.component";

@NgModule({
    declarations: [
        ShipOfferingComponent,
        ShipOfferingDetailComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild([
            { path: 'ship-offering', component: ShipOfferingComponent },
            { path: 'ship-offering/:id', component: ShipOfferingDetailComponent }
        ])
    ]
})
export class ShipOfferingModule { }