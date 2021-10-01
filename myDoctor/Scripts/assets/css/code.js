function hamDropdown() {
    document.querySelector(".noidung_dropdown").classList.toggle("hienThi");
}

window.onclick = function (e) {
    if (!e.target.matches('.nut_dropdown')) {
        var noiDungDropdown = document.querySelector(".noidung_dropdown");
        if (noiDungDropdown.classList.contains('hienThi')) {
            noiDungDropdown.classList.remove('hienThi');
        }
    }
}