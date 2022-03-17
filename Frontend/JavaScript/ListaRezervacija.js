import { Rezervacija } from "./Rezervacija.js";

export class ListaRezervacija {

    rezervacije = [];

    constructor(email) {
        this.email = email;
    }

    loadData() {
        var request = new XMLHttpRequest();
        request.open('GET', "https://localhost:5001/rezervacija/ProveriRezervaciju?email=" + this.email, false);
        request.setRequestHeader('Content-Type', 'application/json');
        request.send(null);

        try {
            let data = JSON.parse(request.response);

            data.forEach(e => {
                this.rezervacije.push(new Rezervacija(e.id, e.klinikaOdeljenje, e.termin, e.datum, e.email));
            })
        }
        catch (Exception)
        {}
    }

    draw(container) {
        if (this.rezervacije.length != 0) {
            this.rezervacije.forEach(e => {
                e.draw(container);
            }
            )
        }
        else if (this.email == "")
        {
            alert("Unesite e-mail!")      
            }
        else {
            alert("Ne postoji rezervacija na ovaj e-mail!")
        }
    }
}