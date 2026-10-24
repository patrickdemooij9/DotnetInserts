$fn=48;
translate([0,0,0])
{
difference()
{
    cube(size = [120.00, 90.00, 25.00], center = false); 
    translate(v = [25.00, 30.00, 5.00])
    {
        union()
        {
            cube(size = [8.67, 14.00, 20.00], center = false); 
            translate(v = [0.00, 16.00, 0.00])
            {
                cube(size = [8.67, 14.00, 20.00], center = false); 
            }
            translate(v = [10.67, 0.00, 0.00])
            {
                cube(size = [8.67, 14.00, 20.00], center = false); 
            }
            translate(v = [10.67, 16.00, 0.00])
            {
                cube(size = [8.67, 14.00, 20.00], center = false); 
            }
            translate(v = [21.33, 0.00, 0.00])
            {
                cube(size = [8.67, 14.00, 20.00], center = false); 
            }
            translate(v = [21.33, 16.00, 0.00])
            {
                cube(size = [8.67, 14.00, 20.00], center = false); 
            }
        }
    }
    translate(v = [88.00, 31.00, 5.00])
    {
        translate(v = [14.00, 14.00, 0.00])
        {
            cylinder(center = false, r1 = 14, r2 = 14, h = 20, $fn = 100);
        }
    }
}

}
