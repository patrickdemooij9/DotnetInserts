$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [100.00, 80.00, 30.00], center = false); 
    translate(v = [2.00, 2.00, 2.00])
    {
        cube(size = [96.00, 76.00, 28.00], center = false); 
    }
}

}
