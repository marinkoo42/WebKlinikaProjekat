import { Klinika } from "./Klinika.js";
import { Odeljenje } from "./Odeljenje.js";
import { ListaTermina } from "./ListaTermina.js";


export class KlinikaOdeljene {

    constructor(id, odeljenje, klinika, lekar) {
        this.id = id;
        this.odeljenje = odeljenje;
        this.klinika = klinika;
        this.lekar = lekar;
    }

    getFromDatabase(id) {
        var request = new XMLHttpRequest();
        request.open('GET', "https://localhost:5001/server/KlinikaOdeljene/GetKlinikaOdeljenjeBy/" + id, false);
        request.setRequestHeader('Content-Type', 'application/json');
        request.send(null);

        let data = JSON.parse(request.response);
        this.id = data[0].id;
        this.klinika = data[0].klinika;
        this.odeljenje = data[0].odeljenje;
        this.lekar = data[0].lekar;
    }

    draw(container) {
        let col1 = document.createElement("div");
        col1.classList.add("col-md-6");
        let col2 = document.createElement("div");
        col2.classList.add("col-md-6");
        let row = document.createElement("div");
        row.classList.add("row");

        let naziv = document.createElement("h5");
        let slika = document.createElement("img");
        slika.src = "\\Frontend\\Images\\" + this.odeljenje.slikaOdeljenja;
        slika.classList.add("img-fluid");

        col2.appendChild(slika);

        naziv.setAttribute("class", "card-title");
        naziv.innerHTML = this.odeljenje.nazivOdeljenja;

        let opis = document.createElement("p");
        opis.setAttribute("class", "card-text");
        opis.innerHTML = this.odeljenje.opisOdeljenja;

        let lekar = document.createElement("p");
        lekar.setAttribute("class", "card-text2");
        lekar.innerHTML = "Doktor: " + this.lekar;

        let dugme = document.createElement("button");
        dugme.innerHTML = "Zakazivanje";
        dugme.setAttribute("class", "btn btn-primary");

        dugme.onclick = () => {

            if (document.getElementById("dtpCol") != null)
            {
                document.querySelector(".dp").removeChild(document.getElementById("dtpCol"));
            }
            let label = document.createElement("p");
            label.innerHTML = `Zakazujete na odeljenju: ${this.odeljenje.nazivOdeljenja}`;
            label.setAttribute("style", "margin-bottom:1rem;");

            let col = document.createElement("div");
            col.id = "dtpCol";
            col.classList.add("datepicker"); 
            let datePicker;
            datePicker = document.createElement("input");
            datePicker.setAttribute("id", "datePckr");
            datePicker.setAttribute("type", "date");

            let today = new Date();                                             //blokira prethodne dane u date pickeru
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0');
            var yyyy = today.getFullYear();
            today = yyyy + '-' + mm + '-' + dd;
            datePicker.setAttribute("min", today);
            datePicker.value = today;

            col.appendChild(label);
            col.appendChild(datePicker);

            document.querySelector(".dp").appendChild(col);
            document.getElementById("datePckr").focus();
            document.getElementById("datePckr").scrollIntoView();


            datePicker.oninput = () =>
            {
                let listaTermina = new ListaTermina(this.id, datePicker.value);
                listaTermina.draw();
            }            
        }

        var cardBody = document.createElement("div");
        cardBody.setAttribute("class", "card-body");


        var card = document.createElement("div");
        card.setAttribute("class", "card");

        col1.appendChild(cardBody);

        row.appendChild(col1);
        row.appendChild(col2);

        card.appendChild(row);

        cardBody.appendChild(naziv);
        cardBody.appendChild(opis);
        cardBody.appendChild(lekar);
        cardBody.appendChild(dugme);

        container.appendChild(card);
    }
}