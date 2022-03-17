//import { Prikaz } from "./Prikaz.js"

import { KlinikaOdeljene } from "./KlinikaOdeljenje.js";

export class ListaKlinikaOdeljenje {

    klinikeOdeljenja = []

    loadData(idKlinike) {
        var request = new XMLHttpRequest();

        request.open('GET', "https://localhost:5001/server/KlinikaOdeljene/GetKlinikaOdeljenje/" + idKlinike, false);
        request.setRequestHeader('Content-Type', 'application/json');
        request.send(null);

        try {
            let data = JSON.parse(request.response);
            data.forEach(e => {
                this.klinikeOdeljenja.push(new KlinikaOdeljene(e.id, e.odeljenje, e.klinika, e.lekar));
            });
        }
        catch (Exception)
        {
        }

    }

    draw(container) {
        if (this.klinikeOdeljenja.length != 0) {
            this.klinikeOdeljenja.forEach(e => {
                e.draw(container);
            })
        } else {
            alert("Ne postoje odeljenja u ovoj klinici!");
        }
    }
}