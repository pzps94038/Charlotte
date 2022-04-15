import { trigger,state,style,animate,transition, group, query } from '@angular/animations';
export const RouterAnimations = [
  trigger('routeAnimations',[
    transition('*=>*',[
      query(':enter',[
        style(
          {
            transform: 'translateY(20%)',
            display: 'block',
            opacity: 0,
            height: '100%'
          }),
        animate(500, style({transform: 'translateY(0%)', opacity: 1}))
      ], {optional: true}),
    ]),
  ])
]
