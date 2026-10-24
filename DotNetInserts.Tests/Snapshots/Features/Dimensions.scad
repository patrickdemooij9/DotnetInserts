$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [70.00, 100.00, 30.00], center = false); 
    translate(v = [1.00, 58.00, 4.00])
    {
        cube(size = [68.00, 40.00, 26.00], center = false); 
    }
}

}
translate([70,0,0])
{
difference()
{
    cube(size = [100.00, 50.50, 30.00], center = false); 
    translate(v = [4.00, 4.00, 6.00])
    {
        cube(size = [92.00, 42.50, 24.00], center = false); 
    }
}

}
