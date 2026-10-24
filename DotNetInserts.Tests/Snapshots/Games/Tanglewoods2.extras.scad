$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [250.00, 142.00, 49.00], center = false); 
    translate(v = [0.75, 0.75, 0.00])
    {
        cube(size = [248.50, 140.50, 46.00], center = false); 
    }
}

}
