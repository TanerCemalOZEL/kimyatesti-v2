var allBtn = document.querySelectorAll(".btnOpt");
var textBox = document.querySelector("#UserAns");
var soruSayisi = (document.querySelector("#accordionExample").children.length) - 1;
var sonSoruMu = false;
var confirmMsg = "Soruların tamamını cevaplamadınız. Kaydetmek istediğinize emin misiniz?";
var testAdi = document.querySelector("#testName").textContent;
// String dizisinde istenen indexteki karakteri değiştirme fonksiyonu.

String.prototype.replaceAt = function (index, char) {
    var a = this.split("");
    a[index] = char;
    return a.join("");
}

allBtn.forEach(element => {
    element.addEventListener("click", clickWorks);
});

function clickWorks() {
    // btnOptSelected classını seçilen butona eklerken, diğer butonlardan kaldırır. 
    // Asıl karta cardClicked classını ekler.
    var clkSiblingList = event.target.parentNode.children;
    for (let i = 0; i < 5; i++) {
        clkSiblingList[i].className = "btnOpt";
    }
    var elementX = event.target;
    var parentCard = elementX.parentNode.parentNode.parentNode.parentNode.parentNode;

    elementX.classList.add("btnOptSelected");
    parentCard.classList.add("cardClicked")

    // gizli textBox ta yer alan değeri alır, tıklanan seçeneği ekleyerek geri gönderir. 
    // Değerleri localStorage üzerine kaydeder.
    var clickedSoruNum = elementX.parentNode.id;
    var clickedCevap = elementX.textContent;
    var textBoxValue = textBox.value;
    var newValue = textBoxValue.replaceAt(clickedSoruNum - 1, clickedCevap);
    textBox.value = newValue;

    localStorage.setItem(('cevapStore' + testAdi), newValue);

    let nextSoru = `#acc${parseInt(clickedSoruNum) + 1}`;
    let thisSoru = `#acc${clickedSoruNum}`;

    // show - hide handler
    if (clickedSoruNum == soruSayisi || document.querySelector(nextSoru) != null ? document.querySelector(nextSoru).parentNode.classList.contains('cardClicked') : true) {
        $(thisSoru).collapse('hide');
    } else {
        $(nextSoru).collapse('show')
    }
}

