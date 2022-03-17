import { KlinikaOdeljene } from "./KlinikaOdeljenje.js";
import { Rezervacija } from "./Rezervacija.js";

export class ListaTermina{

    rezervacije = []

    constructor(id, datum) {
        this.KlinikaOdeljenjeId = id;
        this.datum = datum;
    }

    loadData(selectTermin) {
        fetch('https://localhost:5001/rezervacija/ZauzetiTermini?KlinikaOdeljeneId=' + this.KlinikaOdeljenjeId + '&datum=' + this.datum + 'T00%3A00%3A00.000Z')
            .then(data => data.json())
            .then(data => {
                data.forEach(e => {
                    let rezervacija = new Rezervacija(0, e.klinikaodeljenje, e.termin, e.datum);                
                    this.rezervacije.push(rezervacija);
                })
                let opt = [];
                for (let i = 0; i < selectTermin.length; i++) {
                    opt.push(selectTermin.options[i].value);
                }

                this.rezervacije.forEach(p =>                                           //update opcije u listi termina, briše zauzete!
                {
                    if (p.termin in opt) {
                        for (let i = 0; i < selectTermin.length; i++)
                            if (selectTermin.options[i].id == p.termin)
                                selectTermin.removeChild(selectTermin.options[i]);
                    }
                })
                
            })
            .catch(err => console.error(err));
    }

    draw() {

        let termini = document.querySelector(".termini");
        let forma = document.querySelector(".forma");

        forma.innerHTML = '';
        forma.classList.add("card");

        let label = document.createElement("label");
        label.setAttribute("for", "emailTxt");
        label.innerHTML = "Unesite email na koji želite da zakažete";

        let email = document.createElement("input");
        email.setAttribute("class", "form-control emailTxt");
        email.setAttribute("id", "emailTxt");
        email.setAttribute("type", "email");


        let submit = document.createElement("button");
        submit.setAttribute("class", "btn btn-primary");
        submit.setAttribute("id", "submitDugme");
        submit.innerHTML = "Zakaži!";
        submit.onclick = () => {
            let termin = selectTermin.value;
            if (email.value == "")
                alert("Unesite email!");
            else if (termin != null)
                {
                    let temp = new KlinikaOdeljene();
                    temp.getFromDatabase(this.KlinikaOdeljenjeId);
                    let rezervacija = new Rezervacija(0, temp, termin,this.datum ,email.value);
                    rezervacija.addToDatabase();
              
                alert("Uspešno zakazano!");
                termini.innerHTML = '';
                forma.innerHTML = '';
            } else {
                alert("Svi termini u ovom danu su popunjeni!");
            }
        }

        email.setAttribute("style", "margin-bottom:0.5rem;");
        submit.setAttribute("style", "margin-bottom:0.5rem;");
        label.setAttribute("style", "margin-bottom:0.5rem;");

        forma.appendChild(label);
        forma.appendChild(email);
        forma.appendChild(submit);

        termini.innerHTML = '';

        let selectTermin = document.createElement("select");
        selectTermin.setAttribute("class", "form-select lista-termina");
        selectTermin.setAttribute("aria-label", "Izaberite termin!");
        
        for (let i = 1; i <= 10; i++)
        {
            let option = document.createElement("option");
            option.id = i;
            option.value = i;
            option.innerHTML = `Termin ${i}: ${7 + i}:00 - ${8 + i}:00`;
            selectTermin.appendChild(option);
        }
        this.loadData(selectTermin);

        termini.appendChild(selectTermin);
        document.getElementById("submitDugme").scrollIntoView();


    }
}