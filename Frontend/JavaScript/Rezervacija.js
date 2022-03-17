export class Rezervacija {

    constructor(id, klinikaodeljenje, termin, datum, email) {
        this.id = id;
        this.klinikaodeljenje = klinikaodeljenje;
        this.termin = termin;
        this.datum = datum; 
        this.email = email;
    }

    draw(container) {
        let card = document.createElement("div")
        card.style = "width : 360px";
        card.classList.add("card");

        let cardBody = document.createElement("div");
        cardBody.classList.add("card-body2");


        let Odeljenje = document.createElement("h4");
        let Klinika = document.createElement("h5");
        let Lokacija = document.createElement("p");
        let Datum = document.createElement("p");
        let Termin = document.createElement("p");
        let Email = document.createElement("div");
        Email.classList.add("form-group");

        let inputEmail = document.createElement("input");
        inputEmail.classList.add("form-control");
        inputEmail.value = this.email;
        inputEmail.disabled = true;

        let Kontrole = document.createElement("div");

        Kontrole.classList.add("btn-group");
        Kontrole.ariaLabel = "Kontrole";
        console.log(this);
        Odeljenje.innerText = this.klinikaodeljenje.odeljenje.nazivOdeljenja;
        Klinika.innerText = this.klinikaodeljenje.klinika.nazivKlinike;
        Lokacija.innerText = "Grad: " + this.klinikaodeljenje.klinika.grad.imeGrada + "\n" + "Ulica: " + this.klinikaodeljenje.klinika.adresa; 
        Datum.innerText = "Datum: " + new Date(this.datum).toString().substring(0,10);
        Termin.innerText = "Termin: " + this.termin+ `; ${7+this.termin}:00 - ${8+this.termin}:00`;
        Email.appendChild(inputEmail);

        let menjaj = document.createElement("button");
        menjaj.classList.add("btn");
        menjaj.classList.add("btn-warning");
        menjaj.innerText = "?";

        menjaj.onclick = () => {
            if (menjaj.innerText == "?") {
                inputEmail.disabled = false;
                menjaj.classList.remove("btn-warning");
                menjaj.classList.add("btn-success");
                menjaj.innerText = "✓";
            } else {
                this.updateData(inputEmail.value,card,container);
                inputEmail.disabled = true;
                menjaj.classList.add("btn-warning");
                menjaj.classList.remove("btn-success");
                menjaj.innerText = "?";
            }
        }


        let obrisi = document.createElement("button");
        obrisi.classList.add("btn");
        obrisi.classList.add("btn-danger");
        obrisi.innerText = "X";

        obrisi.onclick = () => { this.removeFromDatabase(card, container); }

        Kontrole.append(menjaj);
        Kontrole.append(obrisi);

        cardBody.appendChild(Odeljenje);
        cardBody.appendChild(Klinika);
        cardBody.appendChild(Lokacija);
        cardBody.appendChild(Datum);
        cardBody.appendChild(Termin);
        cardBody.appendChild(Email);
        cardBody.appendChild(Kontrole);

        card.appendChild(cardBody);
        container.appendChild(card);
    }

    addToDatabase() {
        fetch("https://localhost:5001/rezervacija/PostRezervacija/" + this.klinikaodeljenje.id + "/" + this.termin+ "/"+ this.datum + "/" + this.email
            , { method: 'POST' });
    }

    removeFromDatabase(card, container) {
        fetch("https://localhost:5001/rezervacija/DeleteRezervacija/" + this.id
            , { method: 'DELETE' })
            .then(res => {
                if (res.status == 200) {
                    alert("Zakazani termin je obrisan!");
                    container.removeChild(card);
                } else {
                    alert("Greška pri brisanju zakazanog termina!");
                }
            })
            .catch(err => console.error(err));
    }

    updateData(noviEmail, card, container) {
        if (this.email != noviEmail) {
            fetch("https://localhost:5001/rezervacija/PutRezervacija/" + this.id + "/" + noviEmail,
                { method: 'PUT' })
                .then(res => {
                    if (res.status == 200) {
                        alert("Zakazivanje je ažurirano!");
                        container.removeChild(card);
                    } else {
                        alert("Greška pri ažuriranju zakazivanja!");
                    }
                })
                .catch(err => console.error(err))
        }
        
    }
}