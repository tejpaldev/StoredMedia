$(document).ready(function(){
  
  $( "body" ).delegate( ".body-wrapper", "click", function() {
    slideout.toggle();
  });

  $(".advance-search-more .more").click(function(){
    $(this).addClass('hidden');
    $(this).parent().find('.less').removeClass('hidden');
    $('.keyword-search-out').addClass('more-results');
  });

  $(".advance-search-more .less").click(function(){
    $(this).addClass('hidden');
    $(this).parent().find('.more').removeClass('hidden');
    $('.keyword-search-out').removeClass('more-results');
  });

  $(".common-exp-col-out .more").click(function(){
    $(this).addClass('hidden');
    $(this).parent().find('.less').removeClass('hidden');
    $(this).parent().parent().find('.more-container').removeClass('hidden');
  });

  $(".common-exp-col-out .less").click(function(){
    $(this).addClass('hidden');
    $(this).parent().find('.more').removeClass('hidden');
    $(this).parent().parent().find('.more-container').addClass('hidden');
  });

  $(".slideout-menu ul li a").click(function(){
    $(this).parent().find('.menu-expanded').slideToggle();

    if ($(this).hasClass('icon-plus')) {
      $(this).removeClass('icon-plus').addClass('icon-close');
    } else {
      $(this).removeClass('icon-close');
      $(this).addClass('icon-plus');
    }
    // return false;
  });
});

 


var slideout = new Slideout({
  'panel': document.getElementById('xsPanel'),
  'menu': document.getElementById('xsMenu'),
  'padding': 260,
  'tolerance': 70,
  'side': 'left'
});

slideout.on('open', function() { 
    $('#xsPanel').append('<div class="body-wrapper"></div>');
});

slideout.on('close', function() { 
 $('.body-wrapper').remove();
});
// Toggle button
document.querySelector('.toggle-button').addEventListener('click', function() {
  slideout.toggle();
});


//mobile view
if(window.innerWidth < 767) {
}
    
if(window.innerWidth > 767) {

}

$(window).resize(function () {

  
  //mobile view
  if(window.innerWidth < 767) {

  }

  if(window.innerWidth > 767) {

  }

});

