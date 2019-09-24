$(document).ready(function(){
    $('#likeButton').click(function(e){
        e.preventDefault();
        $('#likeform').submit();
    });

    $('#unlikeButton').click(function(e){
        e.preventDefault();
        $('#unlikeform').submit();
    });

});