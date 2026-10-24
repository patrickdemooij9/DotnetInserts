$fn=48;
translate([0,0,0])
{
difference()
{
    translate(v = [2.00, 2.00, 0.00])
    {
        hull()
        {
            cylinder(center = false, r1 = 2, r2 = 2, h = 25);
            translate(v = [56.00, 0.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 25, $fn = 50);
            }
            translate(v = [0.00, 56.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 25, $fn = 50);
            }
            translate(v = [56.00, 56.00, 0.00])
            {
                cylinder(center = false, r1 = 2, r2 = 2, h = 25, $fn = 50);
            }
        }
    }
    translate(v = [4.00, 4.00, 3.00])
    {
        union()
        {
            translate(v = [0.00, 0.00, 5.50])
            {
                cube(size = [52.00, 52.00, 16.50], center = false); 
            }
            translate(v = [26.00, 0.00, 5.50])
            {
                scale(v = [26.00, 52.00, 5.50])
                {
                    rotate([-90.00, 0.00, 0.00])
                    {
                        cylinder(center = false, r1 = 1, r2 = 1, h = 1, $fn = 50);
                    }
                }
            }
        }
    }
}

}
