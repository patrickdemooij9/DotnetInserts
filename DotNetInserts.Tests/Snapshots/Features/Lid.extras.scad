$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [80.00, 60.00, 6.00], center = false); 
    translate(v = [0.05, 0.05, 0.00])
    {
        cube(size = [79.90, 59.90, 4.00], center = false); 
    }
}

}
