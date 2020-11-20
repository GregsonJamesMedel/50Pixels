$(document).ready(function(){
    $('#likeButton').click(function(e){
        e.preventDefault();
        $('#likeform').submit();
    });

    $('#unlikeButton').click(function(e){
        e.preventDefault();
        $('#unlikeform').submit();
    });

    $('#fileinput').change(function()
    {
        var preview = document.getElementById('setImg');
        var file    = document.getElementById('fileinput').files[0];
        var reader  = new FileReader();
            
        reader.onloadend = function () {
          preview.src = reader.result;
        }
    
        if (file) {
          reader.readAsDataURL(file);
        } else {
          preview.src = "";
        }
    });
    
});