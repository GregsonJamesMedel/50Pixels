window.onload = function()
{
    GalleryPhotoClick();
};

function GalleryPhotoClick()
{
    var gallery = document.getElementsByClassName("Gallery")[0];
    gallery.onclick = function(e)
    {
        e = window.event || event;
        e.preventDefault();
        var targetElement = e.target || e.srcElement;
        DisplayModal(targetElement,e);
    };
}

function DisplayModal(targetElement,e)
{
    var modal = document.getElementById("myModal");
    modal.style.display = "block";
    
    var modalImage = document.getElementById("modal-img");
    modalImage.src = targetElement.getAttribute("src");

    document.getElementById("modal-header").innerHTML = targetElement.getAttribute("data-title");

    CloseModal(modal);
}

function CloseModal(modal)
{
    var closeSpan = document.getElementsByClassName("close")[0];

    closeSpan.onclick = function()
    {
        modal.style.display = "none";
    }

    window.onclick = function(e)
    {
        if(e.target == modal)
        {
            modal.style.display = "none";
        }
    }
}