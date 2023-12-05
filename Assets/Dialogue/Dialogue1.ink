-> main

== main ==
Sor #audio:default

Bana soru sormanı bekliyorum. #audio:repeat

Evet hakla sormadın ? #audio:ragain
    +[ne işle uğraşıyorsun ?]
        ->chosen("Avukatlık yapıyorum.") 
    +[kaç yaşındasın?]
        ->chosen2 ("18 yasındayım ve öğrenciyim")
    +[dün gece 20:00 da ne yapıyordun?]
        ->chosen3 ("Sinema da film izliyordum")

=== chosen(cevap) ===
{cevap}#audio:job

->END
=== chosen2(cevap2) ===
{cevap2}#audio:age

->END
=== chosen3(cevap3) ===
{cevap3}#audio:movie


->END