 function set_name_as_id(attr_name)
 {
     $("[" + attr_name + "]").each(function (e) {
         $(this).attr("name", $(this).attr("id"));
     });
 }